export interface Appointment {
  id?: number;
  patientId: number;
  patient?: any;

  doctorId: number;
  doctor?: any;

  startTime: string; // ISO string e.g. 2025-11-18T11:00:00.922
  endTime: string;

  subject: string;
  description?: string;
  doctorComment?: string;

  isDeleted?: boolean;
  deletedDate?: string | null;
  createdDate?: string;
  modifiedDate?: string | null;
}
