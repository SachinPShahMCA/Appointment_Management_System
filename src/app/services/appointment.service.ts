import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Appointment } from '../models/appointment';
import { ApiService } from './api.service';
import { AppointmentCreateUpdateDto } from '../models/appointment-create-update-dto';

@Injectable({ providedIn: 'root' })
export class AppointmentService {
  private base = 'appointment';

  constructor(private http: ApiService) {}

  getAll(): Observable<Appointment[]> {
    return this.http.get<Appointment[]>(this.base);
  }

  getById(id: number): Observable<Appointment> {
    return this.http.get<Appointment>(`${this.base}/${id}`);
  }

  create(dto: AppointmentCreateUpdateDto): Observable<any> {
    // backend should return JSON; if returns text change responseType to 'text'
    return this.http.post(`${this.base}`, dto);
  }

  update(id: number, dto: AppointmentCreateUpdateDto): Observable<any> {
    return this.http.put(`${this.base}/${id}`, dto);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.base}/?id=${id}`);
  }

  // client-side helper optional: check basic overlap in UI
  isOverlap(existing: Appointment[], doctorId: number, start: Date, end: Date) {
    return existing.some(a => a.doctorId === doctorId &&
      new Date(a.startTime) < end && new Date(a.endTime) > start);
  }
}
