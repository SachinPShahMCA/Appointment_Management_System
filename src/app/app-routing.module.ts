import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorEditComponent } from './components/Doctor/doctor-edit/doctor-edit.component';
import { DoctorListComponent } from './components/Doctor/doctor-list/doctor-list.component'; 
import { DoctorCreateComponent } from './components/Doctor/doctor-create/doctor-create.component';
import { PatientListComponent } from './components/Patient/patient-list/patient-list.component';
import { PatientCreateComponent } from './components/Patient/patient-create/patient-create.component';
import { PatientEditComponent } from './components/Patient/patient-edit/patient-edit.component';
import { AppointmentListComponent } from './components/Appointment/appointment-list/appointment-list.component';
import { AppointmentCreateComponent } from './components/Appointment/appointment-create/appointment-create.component';
import { AppointmentEditComponent } from './components/Appointment/appointment-edit/appointment-edit.component';
// we can also go for module base
const routes: Routes = [
  { path: '', redirectTo: 'appointment', pathMatch: 'full' },
  { path: 'doctor', component: DoctorListComponent },
  { path: 'doctor/create', component: DoctorCreateComponent },
  { path: 'doctor/:id/edit', component: DoctorEditComponent },
  { path: 'Patient', component: PatientListComponent },
  { path: 'patient/create', component: PatientCreateComponent },
  { path: 'patient/:id/edit', component: PatientEditComponent },
  { path: 'appointment', component: AppointmentListComponent },
{ path: 'appointment/create', component: AppointmentCreateComponent },
{ path: 'appointment/:id/edit', component: AppointmentEditComponent },
{ path: '**', redirectTo: 'appointment', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
