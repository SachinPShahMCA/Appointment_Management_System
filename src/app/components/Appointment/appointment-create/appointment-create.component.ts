import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppointmentService } from 'src/app/services/appointment.service';

@Component({
   selector: 'app-appointment-create',
  templateUrl: './appointment-create.component.html'
})
export class AppointmentCreateComponent {
  constructor(private svc: AppointmentService, private router: Router) {}

  save(payload: any) {
    this.svc.create(payload).subscribe({
      next: () => this.router.navigate(['/appointment']),
      error: (e) => console.error(e)
    });
  }

  cancel() { this.router.navigate(['/appointment']); }
}
