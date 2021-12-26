import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-titulo',
  templateUrl: './titulo.component.html',
  styleUrls: ['./titulo.component.scss']
})
export class TituloComponent implements OnInit {
  @Input() titulo = "" as string;
  @Input() subtitulo =  "" as  string;
  @Input() iconClass =  "" as  string;
  @Input() btnListar =  false as  boolean;
  constructor(private router: Router) { }

  ngOnInit() {
  }


  listar(): void {
    this.router.navigate([`/${this.titulo.toLocaleLowerCase()}/lista`]);
  }
}
