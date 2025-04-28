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

  public add(businessCard: FormData) {
    return this.http.post(`${this.apiUrl}/api/businessCards`, businessCard);
  }

  public delete(id: number) {
    return this.http.delete(`${this.apiUrl}/api/businessCards/${id}`);
  }

  public exportToCsv(id: number) {
    return this.http.get(`${this.apiUrl}/api/businessCards/export/csv/${id}`, {
      responseType: 'blob',
    });
  }

  public exportToXml(id: number) {
    return this.http.get(`${this.apiUrl}/api/businessCards/export/xml/${id}`, {
      responseType: 'blob',
    });
  }

  public search(
    term: string,
    searchString: string
  ): Observable<BusinessCard[]> {
    return this.http.get<BusinessCard[]>(
      `${this.apiUrl}/api/businessCards/filter`,
      {
        params: {
          term,
          searchString,
        },
      }
    );
  }

  public importCsv(formData: FormData) {
    return this.http.post(
      `${this.apiUrl}/api/businessCards/import/csv`,
      formData
    );
  }

  public importXml(formData: FormData) {
    return this.http.post(
      `${this.apiUrl}/api/businessCards/import/xml`,
      formData
    );
  }
}
