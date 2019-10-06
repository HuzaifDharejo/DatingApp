import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  baseUrl = 'http://localhost:55314/api/auth/'
constructor(private http: HttpClient) {}
login(model: any) {
  return this.http.post(this.baseUrl + 'login', model).pipe(new Map((response: Map) => {
      const user = response;
      if (user) {
        localStorage.setItem('token', user.token);
      }
    }));
}

}
