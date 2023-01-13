export class Name {
	value: string;

	constructor(value: string) {
		this.value = value;
	}
}

export class EmptyName extends Name {
	constructor() {
		super('');
		
	}
}
