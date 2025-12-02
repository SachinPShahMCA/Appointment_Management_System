// src/app/services/patient.service.ts

import { Injectable } from '@angular/core';
import { Patient } from '../models/patient';
import { PatientCreateUpdateDto } from '../models/PatientCreateUpdateDto';
import { PatientDropdown } from '../models/patient';
import { ApiService } from './api.service';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class PatientService {

  private readonly base = `${environment.apiUrl}/patient`;

  constructor(private api: ApiService) {}

  // GET /patient
  getAll() {
    return this.api.get<Patient[]>(this.base);
  }

  // GET /patient/{id}
  getById(id: number) {
    return this.api.get<Patient>(`${this.base}/${id}`);
  }

  // POST /patient
  create(dto: PatientCreateUpdateDto) {
    return this.api.post<Patient>(this.base, dto);
  }

  // PUT /patient/{id}
  update(id: number, dto: PatientCreateUpdateDto) {
    return this.api.put<Patient>(`${this.base}/${id}`, dto);
  }

  // DELETE /patient/{id}
  delete(id: number) {
    return this.api.delete<void>(`${this.base}/${id}`);
  }

  // GET /patient/dropdown
  getDropdown() {
    return this.api.get<PatientDropdown[]>(`${this.base}/dropdown`);
  }
}
