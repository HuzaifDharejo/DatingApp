import { Photo } from './Photo';

export interface User {
  id: number;
  name: string;
  gender: string;
  age: number;
  knownAs: string;
  created: Date;
  lastActive: Date;
  lookingFor?: string;
  interests?: string;
  introduction?: string;
  city: string;
  country: string;
  photoUrl: string;
  photos?: Photo[];
}
