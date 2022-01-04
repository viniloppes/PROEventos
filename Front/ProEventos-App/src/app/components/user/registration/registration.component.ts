import { Component, OnInit } from '@angular/core';
import { AbstractControl, AbstractControlOptions, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ValidatorField } from 'src/app/helpers/ValidatorField';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  public form!: FormGroup;


  get f(): any{
    return this.form.controls;
  }
  constructor(public fb: FormBuilder) { }

  ngOnInit(): void {
    this.validation();
  }
  validation(): void{
    const formOptions: AbstractControlOptions = {
      validators: ValidatorField.MustMatch('senha', 'confirmarSenha')
    };

    this.form = this.fb.group({
      primeiroNome: ['', Validators.required],
      ultimoNome: ['', Validators.required],
      email: ['', [
        Validators.required,
        Validators.email
      ]],
      userName: ['', Validators.required],
      senha: ['', [
        Validators.required,
        Validators.minLength(6)
      ]],
      confirmarSenha: ['', Validators.required],
    }, formOptions)
  }

}
