import { Component,OnInit } from '@angular/core';
import { DoctorService } from 'src/app/services/doctor.service'; 
import { DoctorDto } from 'src/app/models/doctor'; 
import { Router } from '@angular/router';

@Component({
  selector: 'app-doctor-list',
  templateUrl: './doctor-list.component.html',
  styleUrls: ['./doctor-list.component.css']
})
export class DoctorListComponent implements OnInit {
  doctors: DoctorDto[] = [];
  loading = false;

  constructor(private svc: DoctorService, private router: Router) {}

  ngOnInit() { this.load(); }

  load() {
    this.loading = true;
    this.svc.getAll().subscribe({
      next: res => { this.doctors = res; this.loading = false; },
      error: () => this.loading = false
    });
  }

  create() { this.router.navigate(['/doctor/create']); }
  edit(id?: number) { if (id) this.router.navigate(['/doctor', id, 'edit']); }

  delete(id?: number) {
    if (!id) return;
    if (!confirm('Delete doctor?')) return;
    this.svc.delete(id).subscribe(() => this.load());
  }
}