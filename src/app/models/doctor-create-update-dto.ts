export interface DoctorCreateUpdateDto {
  firstName: string;
  lastName: string;

  gender?: string | null;

  mobile: string;
  email?: string | null;
}
