export interface DoctorDto {
  id: number;

  firstName: string;
  lastName: string;

  fullName: string;  // computed by backend

  mobile: string;
  email: string;

  gender?: string | null;
}
export interface DoctorDropdown {
  id: number;
  fullName: string;
}
