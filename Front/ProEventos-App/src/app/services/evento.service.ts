import { Inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, take } from 'rxjs';
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
    return this.http.get<Evento[]>(this.baseURL).pipe(take(1));
  }
  getEventosByTema(tema: string) : Observable<Evento[]>{
    return this.http.get<Evento[]>(`${this.baseURL}/tema/${tema}`).pipe(take(1));
  }
  getEventoById(id: number) : Observable<Evento>{
    return this.http.get<Evento>(this.baseURL + "/id/" + id).pipe(take(1));
  }

  public post(evento: Evento) : Observable<Evento>{
    return this.http.post<Evento>(this.baseURL, evento);
  }
  public put( evento: Evento) : Observable<Evento[]>{
    return this.http.put<Evento[]>(`${this.baseURL}/${evento.id}`, evento).pipe(take(1));
  }

  public deleteEvento(id:number) : Observable<any>{
    return this.http.delete<any>(`${this.baseURL}/${id}`).pipe(take(1));
  }
}
