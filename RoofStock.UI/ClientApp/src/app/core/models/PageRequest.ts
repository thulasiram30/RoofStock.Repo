export class PageRequest {
  pageNumber: number;
  pageSize: number;
  constructor() {
    this.pageNumber = 1;
    this.pageSize = 10;
  }
}
