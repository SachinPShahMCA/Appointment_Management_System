import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from 'src/app/services/patient.service';
import { Patient } from 'src/app/models/patient';

@Component({
  templateUrl: './patient-edit.component.html'
})
export class PatientEditComponent implements OnInit {

  patient?: Patient;

  constructor(
    private route: ActivatedRoute,
    private svc: PatientService,
    private router: Router
  ) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.svc.getById(id).subscribe(res => this.patient = res);
  }
 
  save(dto: any) {
    this.svc.update(this.patient!.id!, dto)
      .subscribe(() => this.router.navigate(['/Patient']));
  }
  cancel() {
  this.router.navigate(['/Patient']);
}

}
