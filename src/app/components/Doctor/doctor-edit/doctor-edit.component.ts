import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DoctorService } from 'src/app/services/doctor.service';
import { DoctorDto } from 'src/app/models/doctor';
import { DoctorCreateUpdateDto } from 'src/app/models/doctor-create-update-dto';

@Component({
  selector: 'app-doctor-edit',
  templateUrl: './doctor-edit.component.html',
  styleUrls: ['./doctor-edit.component.css']
})
export class DoctorEditComponent implements OnInit {
   doctor?: DoctorDto;
  loading = false;
 constructor(
    private route: ActivatedRoute,
    private svc: DoctorService,
    private router: Router
  ) {}

 ngOnInit() {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) return;
    this.loading = true;
    this.svc.getById(id).subscribe({
      next: res => { this.doctor = res; this.loading = false; },
      error: () => this.loading = false
    });
  }

  save(dto: DoctorCreateUpdateDto) {
      console.log("DTO RECEIVED:", dto);
      debugger
    if (!this.doctor) return;
    this.svc.update(this.doctor.id, dto).subscribe(() => this.router.navigate(['/doctor']));
  }

  cancel() {
  this.router.navigate(['/doctors']);
}

}
