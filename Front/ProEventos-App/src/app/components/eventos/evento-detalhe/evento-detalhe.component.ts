import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Evento } from '@app/models/Evento';
import { EventoService } from '@app/services/evento.service';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';

import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-evento-detalhe',
  templateUrl: './evento-detalhe.component.html',
  styleUrls: ['./evento-detalhe.component.scss'],
})
export class EventoDetalheComponent implements OnInit {
  form!: FormGroup;
  evento = {} as Evento;
  public OnEdit: boolean;

  get f(): any {
    return this.form.controls;
  }
  get bsConfig(): any {
    return {
      isAnimated: true,
      adaptivePosition: true,
      dateInputFormat: 'DD/MM/YYYY hh:mm a',
      containerClass: 'theme-default',
      showWeekNumbers: false,
    };
  }
  constructor(
    private fb: FormBuilder,
    private localeService: BsLocaleService,
    private activateRouter: ActivatedRoute,
    private eventoService: EventoService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
    private router: Router
  ) {
    this.localeService.use('pt-br');
  }

  ngOnInit(): void {
    this.validation();
    this.carregarEvento();
  }

  public carregarEvento() {
    const eventoIdParam = this.activateRouter.snapshot.paramMap.get('id');
    if (eventoIdParam !== null) {
      this.OnEdit = true;
      this.eventoService.getEventoById(+eventoIdParam).subscribe({
        next: (evento: Evento) => {
          this.evento = { ...evento };
          // this.toastr.error("Falha ao tentar carregar evento", "Erro!");
          this.form.patchValue(this.evento);
          this.spinner.hide();
        },
        error: (err: any) => {
          this.spinner.hide();
          console.log(err.error);
        },
        complete: () => {
          this.spinner.hide();
        },
      });
    } else {
      this.OnEdit = false;

      this.spinner.hide();
    }
  }
  public validation(): void {
    this.form = this.fb.group({
      tema: [
        '',
        [
          Validators.required,
          Validators.minLength(4),
          Validators.maxLength(50),
        ],
      ],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(120000)]],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      imagemURL: ['', Validators.required],
    });
  }

  public resetForm(): void {
    this.form.reset();
  }

  public cssValidator(campo: FormControl): any {
    return { 'is-invalid': this.f.tema.errors && this.f.tema.touched };
  }

  public salvarAlteracao() {
    this.spinner.show();
    if (this.form.valid) {
      if (this.OnEdit == false) {
        this.evento = { ...this.form.value };
        this.eventoService.post(this.evento).subscribe(
          () => this.toastr.success('Evento salvo com sucesso', 'Sucesso!'),
          (err: any) => {
            console.error(err.erro);
            this.spinner.hide();
            this.toastr.error('Falha ao salvar evento', 'Erro!');
          },
          () => {
            this.spinner.hide();
          }
        );
      } else {
        this.evento = {id: this.evento.id, ...this.form.value };

        this.eventoService['put'](this.evento).subscribe(
          () => {
            this.toastr.success('Evento salvo com sucesso', 'Sucesso!');
            this.router.navigate(['/eventos']);
          },
          (err: any) => {
            console.error(err.error);
            this.spinner.hide();
            this.toastr.error('Falha ao salvar evento', 'Erro!');
          },
          () => {
            this.spinner.hide();
          }
        );
      }
    }
  }
}
