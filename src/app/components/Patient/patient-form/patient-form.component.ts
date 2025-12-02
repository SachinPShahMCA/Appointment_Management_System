import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';
import { Patient } from 'src/app/models/patient';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html'
})
export class PatientFormComponent implements OnInit {

  @Input() model!: Partial<Patient>;
  @Output() submitForm = new EventEmitter<Patient>();
  @Output() cancel = new EventEmitter<void>();

  form!: FormGroup;

  constructor(private fb: FormBuilder) {}

  ngOnInit() {
    this.form = this.fb.group({
      firstName: [
        this.model?.firstName || '',
        [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
      ],
      lastName: [
        this.model?.lastName || '',
        [Validators.required, Validators.minLength(2), Validators.maxLength(50)]
      ],
      mobile: [
        this.model?.mobile || '',
        [
          Validators.required,
          Validators.pattern(/^[0-9]{10}$/) // 7 to 15 digits
        ]
      ],
      email: [
        this.model?.email || '',
        [Validators.required, Validators.email]
      ],
      dateOfBirth: [
        this.toDateInputValue(this.model?.dateOfBirth),
        [Validators.required, this.dateNotInFuture]
      ],
      gender: [
        this.model?.gender || '',
        Validators.required
      ],
      isActive: [
        this.model?.isActive ?? true
      ]
    });
  }

  dateNotInFuture(control: AbstractControl) {
  if (!control.value) return null;

  const [year, month, day] = control.value.split('-').map(Number);
  const dob = new Date(year, month - 1, day);
  const today = new Date();
  today.setHours(0, 0, 0, 0);

  return dob > today ? { futureDate: true } : null;
}

toDateInputValue(date?: string): string {
  if (!date) return '';

  const d = new Date(date);
  const year = d.getFullYear();
  const month = (d.getMonth() + 1).toString().padStart(2, '0');
  const day = d.getDate().toString().padStart(2, '0');

  return `${year}-${month}-${day}`;
}

  save() {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.submitForm.emit(this.form.value);
  }
}
