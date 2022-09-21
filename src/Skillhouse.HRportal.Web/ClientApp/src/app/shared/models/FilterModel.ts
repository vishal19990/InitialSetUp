export class FilterModel {
    pageNumber: number;
    sortMember: string;
    pageSize: number;
    sortDescending: boolean;
    filters: Filter[];
  }
  
  export class Filter {
    value: string;
    property: string;
    operator: string;
  }
  