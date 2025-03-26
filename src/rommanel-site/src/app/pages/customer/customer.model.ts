import { Address } from "./address.model";

export interface Customer {
  id?: number;
  name: string;
  document: string;
  birthDate: string;
  phone: string;
  email: string;
  address: Address,
  taxExempt: boolean,
  stateRegistration: string,
}
