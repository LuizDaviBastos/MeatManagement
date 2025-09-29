export interface Address {
    cityId?: string;
    stateId?: string;
  }
  
  export interface Buyer {
    id?: string;
    name?: string;
    document?: string;
    address?: Address;
  }
  