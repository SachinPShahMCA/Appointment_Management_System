import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Appointment } from 'src/app/models/appointment';
import { PatientDropdown } from 'src/app/models/patient';
import { DoctorService } from 'src/app/services/doctor.service';
import { PatientService } from 'src/app/services/patient.service';
import { DoctorDropdown } from 'src/app/models/doctor';

@Component({
  selector: 'app-appointment-form',
  templateUrl: './appointment-form.component.html',
  styleUrls: ['./appointment-form.component.css']
})
export class AppointmentFormComponent implements OnInit {

  @Input() model!: Partial<Appointment>; // for edit
  @Input() existingAppointments: Appointment[] = []; // optional, for client-side overlap hints
  @Output() submitForm = new EventEmitter<any>();
  @Output() cancel = new EventEmitter<void>();

  form!: FormGroup;
patientList: PatientDropdown[] = [];
doctorList: DoctorDropdown[] = [];

  constructor(private fb: FormBuilder,
  private patientSvc: PatientService,
  private doctorSvc: DoctorService) {}

  ngOnInit() {
     this.loadDropdowns();

    this.form = this.fb.group({
      patientId: [this.model?.patientId ?? '', Validators.required],
      doctorId: [this.model?.doctorId ?? '', Validators.required],
      startTime: [this.toDatetimeLocal(this.model?.startTime), Validators.required],
      endTime: [this.toDatetimeLocal(this.model?.endTime), Validators.required],
      subject: [this.model?.subject ?? '', [Validators.required, Validators.minLength(3), Validators.maxLength(100)]],
      description: [this.model?.description ?? '', Validators.maxLength(1000)],
      doctorComment: [this.model?.doctorComment ?? '', Validators.maxLength(1000)]
    }, { validators: this.appointmentValidator });
  }

  // convert ISO to 'yyyy-MM-ddTHH:mm' (datetime-local)
  toDatetimeLocal(value?: string | null): string | null {
    if (!value) return null;
    // handle backend values like "2025-11-18T11:00:00.922"
    const dt = new Date(value);
    if (isNaN(dt.getTime())) return null;
    // return format yyyy-MM-ddTHH:mm
    const pad = (n: number) => String(n).padStart(2, '0');
    return `${dt.getFullYear()}-${pad(dt.getMonth()+1)}-${pad(dt.getDate())}T${pad(dt.getHours())}:${pad(dt.getMinutes())}`;
  }


parseLocal(value: string): Date {
  const [date, time] = value.split('T');
  const [y, m, d] = date.split('-').map(Number);
  const [hh, mm] = time.split(':').map(Number);
  return new Date(y, m - 1, d, hh, mm, 0);
}

  loadDropdowns() {
  this.patientSvc.getDropdown()
    .subscribe(res => this.patientList = res);

  this.doctorSvc.getDropdown()
    .subscribe(res => this.doctorList = res);
}
appointmentValidator = (group: AbstractControl) => {
  const s = group.get('startTime')?.value;
  const e = group.get('endTime')?.value;

  if (!s || !e) return null;

  const start = this.parseLocal(s);
  const end = this.parseLocal(e);

  // Not in past
  if (start < new Date()) return { pastAppointment: true };

  // Start < End
  if (start >= end) return { invalidRange: true };

  // Duration <= 1 hour
  const durationHours = (end.getTime() - start.getTime()) / (1000 * 60 * 60);
  if (durationHours > 1 + 0.0001) return { durationError: true };

  // Office hours validation
  const officeStart = 10;
  const officeEnd = 17;

  if (start.getHours() < officeStart || start.getHours() > officeEnd) {
    return { officeHours: true };
  }

  if (end.getHours() < officeStart || end.getHours() > officeEnd) {
    return { officeHours: true };
  }

  if (end.getHours() === officeEnd && end.getMinutes() > 0) {
    return { officeHours: true };
  }

  return null;
};



  onSave() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    // clean values, ensure ISO strings for backend
    const raw = this.form.value;
    const payload = {
      ...raw
    };
    this.submitForm.emit(payload);
  }
}
