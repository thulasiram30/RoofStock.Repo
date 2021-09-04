import { BaseEntity } from "./base";

export class PageResponse<T extends BaseEntity> {
    pageNumber: number = 0;
    pageSize: number = 0;
    totalNumberOfRecords: number = 0;
    results: T[] = [];
}
