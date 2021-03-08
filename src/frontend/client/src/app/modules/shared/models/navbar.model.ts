export interface INavBarItem {
    title: string;
    url: string;
    active: boolean;
}

export class NavBarItem implements INavBarItem {
    title: string;
    url: string;
    active: boolean;

    constructor(title: string, url: string, active: boolean) {
        this.title = title;
        this.url = url;
        this.active = active;
    }
}