import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthenticateService } from 'src/app/core/services/authenticate.service';

@Component({
  selector: 'app-weblogin',
  templateUrl: './weblogin.component.html',
  styleUrls: ['./weblogin.component.css', '../../../assets/css/webstyle.css']
})
export class WebloginComponent implements OnInit {
  webLoginForm!: FormGroup;
  constructor(private fb: FormBuilder, private authen: AuthenticateService) {
    this.webLoginForm = this.fb.group({
      email: ['', [Validators.required]],
      password: ['', [Validators.required]]
    })
  }

  ngOnInit(): void {
  }
  onSubmit(): void{
    console.log(this.webLoginForm.value.email + " "+ this.webLoginForm.value.password)
    let email = this.webLoginForm.value.email;
    let password = this.webLoginForm.value.password;
    this.authen.webLogin(email, password);
  }
}
