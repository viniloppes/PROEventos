import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
})
export class EventosComponent implements OnInit {
  public arrayEvento: any = [];
  public marginImg: number = 2;
  public widthImg: number = 150;
  public isCollapsed : boolean = true;
  public arrayEventoFiltrado: any = [];
  public showImg: boolean = true;

  private _filtroLista: string = "";

  public get filtroLista() {
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.arrayEventoFiltrado = this._filtroLista ? this.filtrarEventos(this.filtroLista) : this.arrayEvento;
  }

  public filtrarEventos(filtrarPor: string): any {

    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.arrayEvento.filter(
      (evento: {tema: string; local: string;}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }
  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getEventos();
  }

  alterarImg() {
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    this.http.get('https://localhost:5001/api/Evento').subscribe(
      dado_json => {
        this.arrayEvento = dado_json;
        console.log(dado_json);
        this.arrayEventoFiltrado = dado_json;
      }
      ,
      err => {
        console.log(err.error);
      });
  }
}
