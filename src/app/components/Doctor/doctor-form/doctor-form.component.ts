import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { DoctorDto } from 'src/app/models/doctor';

@Component({
  selector: 'app-doctor-form',
  templateUrl: './doctor-form.component.html',
  styleUrls: ['./doctor-form.component.css']
})
export class DoctorFormComponent implements OnInit {

  @Input() model!: Partial<DoctorDto>;
  @Output() submitForm = new EventEmitter<DoctorDto>();

  form!: FormGroup;
   constructor(private fb: FormBuilder) {}

    ngOnInit() {
    this.form = this.fb.group({
      firstName: [this.model?.['firstName'] || '', [Validators.required, Validators.minLength(2)]],
      lastName: [this.model?.['lastName'] || '', Validators.required],
      gender: [this.model?.['gender'] || '', Validators.required],
      mobile: [this.model?.['mobile'] || '', [Validators.required, Validators.pattern(/^[0-9]{10}$/)]],
      email: [this.model?.['email'] || '', Validators.email]
    });
  }

  onSubmit() {
    
     console.log("FORM SUBMITTED");
  console.log("VALID? ", this.form.valid);
  console.log("ERRORS: ", this.form.errors);
  console.log("CONTROLS: ", this.form.controls);
  Object.keys(this.form.controls).forEach(key => {
  const control = this.form.get(key);
  console.log(key, control?.errors);
});

    
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }
    this.submitForm.emit(this.form.value);
  }

}
