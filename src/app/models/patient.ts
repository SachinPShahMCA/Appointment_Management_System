export interface Patient {
  id?: number;
  firstName: string;
  lastName: string;
  fullName?: string;
  mobile: string;
  email: string;
  dateOfBirth: string;
  gender: string;
  isActive: boolean;

  appointments?: any[];
  isDeleted?: boolean;
  deletedDate?: string | null;
  createdDate?: string;
  modifiedDate?: string | null;
}
export interface PatientDropdown {
  id: number;
  fullName: string;
}