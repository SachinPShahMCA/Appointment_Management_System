import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from 'src/app/services/doctor.service';
import { DoctorCreateUpdateDto } from 'src/app/models/doctor-create-update-dto';

@Component({
  selector: 'app-doctor-create',
  templateUrl: './doctor-create.component.html'
})
export class DoctorCreateComponent {

    constructor(private svc: DoctorService, private router: Router) { }
save(dto: DoctorCreateUpdateDto) {
    this.svc.create(dto).subscribe(() => this.router.navigate(['/doctor']));
  }
  cancel() {
  this.router.navigate(['/doctors']);
}
}
