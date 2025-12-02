import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { CoreModule } from './core/core.module';

import { DoctorFormComponent } from './components/Doctor/doctor-form/doctor-form.component';
import { DoctorListComponent } from './components/Doctor/doctor-list/doctor-list.component';
import { DoctorCreateComponent } from './components/Doctor/doctor-create/doctor-create.component';
import { DoctorEditComponent } from './components/Doctor/doctor-edit/doctor-edit.component';
import { PatientListComponent } from './components/Patient/patient-list/patient-list.component';
import { PatientCreateComponent } from './components/Patient/patient-create/patient-create.component';
import { PatientEditComponent } from './components/Patient/patient-edit/patient-edit.component';
import { PatientFormComponent } from './components/Patient/patient-form/patient-form.component';
import { AppointmentListComponent } from './components/Appointment/appointment-list/appointment-list.component';
import { AppointmentCreateComponent } from './components/Appointment/appointment-create/appointment-create.component';
import { AppointmentEditComponent } from './components/Appointment/appointment-edit/appointment-edit.component';
import { AppointmentFormComponent } from './components/Appointment/appointment-form/appointment-form.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';  // <-- Required

@NgModule({
  declarations: [
    AppComponent,   
    DoctorFormComponent,
    DoctorListComponent,
    DoctorCreateComponent,
    DoctorEditComponent,
    PatientListComponent,
    PatientCreateComponent,
    PatientEditComponent,
    PatientFormComponent,
    AppointmentFormComponent,
    AppointmentListComponent,
    AppointmentCreateComponent,
    AppointmentEditComponent
    
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    ReactiveFormsModule,
    CoreModule,
    BrowserAnimationsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
