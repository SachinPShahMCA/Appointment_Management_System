export interface AppointmentCreateUpdateDto {
  patientId: number;
  doctorId: number;
  startTime: string; // ISO/HTML datetime-local expected
  endTime: string;
  subject: string;
  description?: string;
  doctorComment?: string;
}
