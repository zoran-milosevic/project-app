import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { environment } from '@environments/environment';

import { IText } from '@shared/models/text.model';
import { ITextCount } from '@shared/models/count.model';

@Injectable()
export class TextService {

    constructor(private http: HttpClient) {

    }

    getText() {
        const queryUrl = `${environment.apiUrl}/text`;

        return this.http.get<IText>(queryUrl);
    }

    getTextCount(text: string) {
        const url = `${environment.apiUrl}/text/count`;
        const headers = { 'Content-Type': 'application/json' };
        const body = { text: text };

        return this.http.post<ITextCount>(url, body, { headers });
    }
}