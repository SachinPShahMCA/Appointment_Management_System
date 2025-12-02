import { Component, OnInit } from '@angular/core';
import { AppointmentService } from 'src/app/services/appointment.service';
import { Appointment } from 'src/app/models/appointment';
import { Router } from '@angular/router';

@Component({
  selector: 'app-appointment-list',
  templateUrl: './appointment-list.component.html'
})
export class AppointmentListComponent implements OnInit {
  appointments: Appointment[] = [];

  constructor(private svc: AppointmentService, private router: Router) {}

  ngOnInit() { this.load(); }

  load() { this.svc.getAll().subscribe(res => this.appointments = res); }

  edit(id?: number) { if (id) this.router.navigate(['/appointment', id, 'edit']); }
createAppointment() {
  this.router.navigate(['/appointment/create']);
}
  delete(id?: number) {
    if (!id) return;
    if (!confirm('Delete appointment?')) return;
    this.svc.delete(id).subscribe(() => this.load());
  }
  

}
