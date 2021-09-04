
export abstract class BaseEntity {
  id: number = 0;
  createdOn!: Date;
  modifiedOn!: Date;
}
