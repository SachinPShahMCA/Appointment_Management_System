import { Component } from '@angular/core';
import { PatientService } from 'src/app/services/patient.service';
import { Router } from '@angular/router';

@Component({
  templateUrl: './patient-create.component.html'
})
export class PatientCreateComponent {

  constructor(private svc: PatientService, private router: Router) {}

  save(dto: any) {
    this.svc.create(dto).subscribe(() => this.router.navigate(['/Patient']));
  }
  cancel() {
  this.router.navigate(['/Patient']);
}

}
