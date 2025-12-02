// src/app/services/doctor.service.ts
import { Injectable } from '@angular/core';
import { DoctorDto } from '../models/doctor';
import { DoctorCreateUpdateDto } from '../models/doctor-create-update-dto';
import { DoctorDropdown } from '../models/doctor';
import { ApiService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  private readonly base = `doctor`;

  constructor(private api: ApiService) {}

  // GET /doctor
  getAll() {
    return this.api.get<DoctorDto[]>(this.base);
  }

  // GET /doctor/{id}
  getById(id: number) {
    return this.api.get<DoctorDto>(`${this.base}/${id}`);
  }

  // POST /doctor
  create(dto: DoctorCreateUpdateDto) {
    return this.api.post<DoctorDto>(this.base, dto);
  }

  // PUT /doctor/{id}
  update(id: number, dto: DoctorCreateUpdateDto) {
    return this.api.put<DoctorDto>(`${this.base}/${id}`, dto);
  }

  // DELETE /doctor/{id}
  delete(id: number) {
    return this.api.delete<void>(`${this.base}/${id}`);
  }

  // GET /doctor/dropdown
  getDropdown() {
    return this.api.get<DoctorDropdown[]>(`${this.base}/dropdown`);
  }
}
