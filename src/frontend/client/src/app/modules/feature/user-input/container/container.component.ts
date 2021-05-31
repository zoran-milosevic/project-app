import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

import { TextService } from '@core/services';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css'],
  providers: [TextService]
})
export class ContainerComponent implements OnInit {

  text!: string;
  count!: number;

  textForm!: FormGroup;
  submitted = false;
  error!: string;

  constructor(private http: TextService, private formBuilder: FormBuilder) {

  }

  get f() { return this.textForm.controls; }

  onSubmit() {
    this.submitted = true;
    this.text = this.f.text.value;

    if (this.textForm.invalid) {
      return;
    }

    this.http.getTextCount(this.text).subscribe({
      next: data => {
        this.count = data.textLength;
      },
      error: error => {
        this.error = error.message;
      }
    });
  }

  ngOnInit(): void {
    this.textForm = this.formBuilder.group({
      text: ['', Validators.required]
    });
  }

}
