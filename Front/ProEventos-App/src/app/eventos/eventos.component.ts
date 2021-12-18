import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public arrayEvento: any;
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/Evento').subscribe(
      dado_json => {
        this.arrayEvento = dado_json;
      }
      ,
      err => {
        console.log(err.error);
      });
  }
}
