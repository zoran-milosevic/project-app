import { Component, OnInit } from '@angular/core';

import { TextService, LogService } from '@core/services';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css'],
  providers: [TextService]
})
export class ContainerComponent implements OnInit {

  text!: string;
  count!: number;

  error!: string;

  fileToUpload!: File;

  constructor(private log: LogService, private http: TextService) {

  }

  countNumberOfWords() {
    this.http.getTextCount(this.text).subscribe({
      next: data => {
        this.count = data.textLength;

        this.log.inspect(data);
      },
      error: error => {
        this.error = error.message;

        this.log.inspect(error);
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
