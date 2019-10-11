import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../_models/User';


const HttpOptions = {
  headers: new HttpHeaders({
    Authoraziton: localStorage.getItem('token'),
  })
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getUsers(): Observable<User[]> {
 return this.http.get<User[]>(this.baseUrl + 'users', HttpOptions);
}
getUser(Id: any): Observable<User> {
  return this.http.get<User>(this.baseUrl + 'users' + Id, HttpOptions);


}
}
