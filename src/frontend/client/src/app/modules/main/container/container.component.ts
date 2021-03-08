import { Component, OnInit } from '@angular/core';

import { TextService } from '@app/modules/shared/providers/text.service';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent implements OnInit {

  text!: string;
  count!: number;
  errorMessage!: string;

  constructor(private http: TextService) {
    this.http.getText().subscribe(res => {
      this.text = res.text;

      this.http.getTextCount(this.text).subscribe({
        next: data => {
          this.count = data.textLength;
        },
        error: error => {
          this.errorMessage = error.message;
        }
      });
    });
  }

  ngOnInit(): void {

  }

}
