import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../models/Evento';

@Injectable({
  providedIn: 'root',
})
export class EventoService {
  baseURL: string = "https://localhost:5001/api/eventos";
  constructor(private http: HttpClient) {
    // this.baseURL = baseUrl;
  }

  getEventos() : Observable<Evento[]>{
    return this.http.get<Evento[]>(this.baseURL);
  }
  getEventosByTema(tema: string) : Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/tema/${tema}`);
  }
  getEventoById(id: number) : Observable<Evento>{
    return this.http.get<Evento>(this.baseURL + "/id/" + id);
  }

}
