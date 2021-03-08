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

  error!: string;

  fileToUpload!: File;

  constructor(private http: TextService) {

  }

  countNumberOfWords() {
    this.http.getTextCount(this.text).subscribe({
      next: data => {
        this.count = data.textLength;
      },
      error: error => {
        this.error = error.message;
      }
    });
  }

  handleFileInput(event: any) {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.readAsText(file);

    reader.onload = () => {
      this.updateText(reader.result);
    };

    reader.onerror = function () {
      console.log(reader.error);
    };
  }

  updateText(text: any) {
    this.text = text;
  }

  ngOnInit(): void {

  }

}
