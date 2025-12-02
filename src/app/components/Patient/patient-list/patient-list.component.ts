import { Component, OnInit } from '@angular/core';
import { PatientService } from 'src/app/services/patient.service';
import { Patient } from 'src/app/models/patient';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html'
})
export class PatientListComponent implements OnInit {

  patients: Patient[] = [];

  constructor(private svc: PatientService, private router: Router) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.svc.getAll().subscribe(res => this.patients = res);
  }

  edit(id: number) {
    this.router.navigate(['/patient', id, 'edit']);
  }

  delete(id: number) {
    if (!confirm('Delete patient?')) return;
    this.svc.delete(id).subscribe(() => this.load());
  }
  create() { this.router.navigate(['/patient/create']); }

}
