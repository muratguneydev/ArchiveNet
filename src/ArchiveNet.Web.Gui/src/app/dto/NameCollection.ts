import { Name } from "./Name";

export class NameCollection {
	names: Name[];

	constructor(names: Name[]) {
		this.names = names;
	}
}

export class EmptyNameCollection extends NameCollection {
	constructor() {
		super([]);
		
	}
}