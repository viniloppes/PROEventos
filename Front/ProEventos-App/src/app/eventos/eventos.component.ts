import { HttpClient } from '@angular/common/http';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { Evento } from '../models/Evento';
import { EventoService } from '../services/evento.service';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.scss'],
  // providers: [EventoService]
})
export class EventosComponent implements OnInit {
  public arrayEvento: Evento[] = [];
  public arrayEventoFiltrado: Evento[] = [];

  public marginImg: number = 2;
  public widthImg: number = 150;
  public isCollapsed : boolean = true;
  public showImg: boolean = true;

  private _filtroLista: string = "";
  modalRef = {} as BsModalRef;

  public get filtroLista() {
    return this._filtroLista;
  }

  public set filtroLista(value: string){
    this._filtroLista = value;
    this.arrayEventoFiltrado = this._filtroLista ? this.filtrarEventos(this.filtroLista) : this.arrayEvento;
  }
  constructor(
    private eventoServico: EventoService,
    private modalService: BsModalService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService) {}

  public filtrarEventos(filtrarPor: string): Evento[] {

    filtrarPor = filtrarPor.toLocaleLowerCase();
    return this.arrayEvento.filter(
      (evento: {tema: string; local: string;}) => evento.tema.toLocaleLowerCase().indexOf(filtrarPor) !== -1 ||
      evento.local.toLocaleLowerCase().indexOf(filtrarPor) !== -1
    )
  }

  ngOnInit(): void {
    this.getEventos();
     /** spinner starts on init */
     this.spinner.show();


  }

  public alterarImg() {
    this.showImg = !this.showImg;
  }

  public getEventos(): void {
    const observer = {
      next: (dado_json: Evento[]) => {
        this.arrayEvento = dado_json;
        console.log(dado_json);
        this.arrayEventoFiltrado = dado_json;
      },
      error: (err: any) => {
        console.log(err.error);
        this.spinner.hide();
        this.toastr.error('Erro ao carregar os eventos', 'Erro!');

      },
      complete: () => {
        this.spinner.hide();

      }
    }
  this.eventoServico.getEventos().subscribe(observer)
  //     (dado_json: Evento[]) => {
  //       this.arrayEvento = dado_json;
  //       console.log(dado_json);
  //       this.arrayEventoFiltrado = dado_json;
  //     }
  //     ,
  //     err => {
  //       console.log(err.error);
  //     });
  }



  public openModal(template: TemplateRef<any>): void {
    this.modalRef = this.modalService.show(template, {class: 'modal-sm'});
  }

  confirm(): void {

    this.modalRef.hide();
    this.toastr.success('O evento foi deletado com sucesso', 'Deletado!');
  }

  decline(): void {
    this.modalRef.hide();
  }
}
