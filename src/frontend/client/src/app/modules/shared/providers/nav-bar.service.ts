import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

import { INavBarItem } from '@app/modules/shared/models/navbar.model';

@Injectable({ providedIn: 'root' })
export class NavBarService {
    public items: BehaviorSubject<INavBarItem[]> = new BehaviorSubject<INavBarItem[]>([]);

    constructor() {

        this.getItems();
    }

    private getItems(): void {

        const items = <INavBarItem[]>[
            { title: 'Database', url: '/database', active: true },
            { title: 'File', url: '/file', active: false },
            { title: 'User Input', url: '/user-input', active: false }
        ];

        this.items.next(items);
    }
}