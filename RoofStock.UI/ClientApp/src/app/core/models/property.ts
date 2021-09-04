import { Address } from "./address";
import { BaseEntity } from "./base";

export class Property extends BaseEntity {
  yearBuilt: number = 0;
  listPrice: number = 0;
  monthlyRent: number = 0;
  grossYieldPercentage: number = 0;
  address: Address = new Address();
}
