import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Observable } from 'rxjs';
import { BusinessCard } from '../types/types';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private http: HttpClient) {}

  private apiUrl: string = environment.apiUrl;

  public getAll(): Observable<BusinessCard[]> {
    return this.http.get<BusinessCard[]>(`${this.apiUrl}/api/businessCards`);
  }
}
