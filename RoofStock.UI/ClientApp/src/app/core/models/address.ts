import { BaseEntity } from "./base";

export class Address extends BaseEntity {
  address1: string = "";
  address2: string = "";
  city: string = "";
  country: string = "";
  county: string = "";
  district: string = "";
  state: string = "";
  zip: number = 0;
  zipPlus4: number = 0;
}
