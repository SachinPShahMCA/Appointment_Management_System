import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AppointmentService } from 'src/app/services/appointment.service';
import { Appointment } from 'src/app/models/appointment';

@Component({
  selector: 'app-appointment-edit',
  templateUrl: './appointment-edit.component.html'
})
export class AppointmentEditComponent implements OnInit {
  appointment?: Appointment;

  constructor(private route: ActivatedRoute, private svc: AppointmentService, private router: Router) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.svc.getById(id).subscribe(res => {
      this.appointment = {
        ...res
      };
    });
  }

  save(payload: any) {
    this.svc.update(this.appointment!.id!, payload).subscribe(() => this.router.navigate(['/appointments']));
  }

  cancel() { this.router.navigate(['/appointment']); }
}
