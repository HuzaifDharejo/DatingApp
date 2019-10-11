import { Photo } from './Photo';

export interface User {
  id: number;
  Name: string;
  Gender: string;
  age: number;
  KnownAs: string;
  Created: Date;
  LastActive: Date;
  LookingFor?: string;
  Interests?: string;
  Introduction?: string;
  City: string;
  Country: string;
  photos?: Photo[];
}
