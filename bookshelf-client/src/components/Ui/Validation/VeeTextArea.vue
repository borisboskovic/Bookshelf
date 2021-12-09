<template>
	<textarea
		rows="3"
		:name="name"
		:class="className ?? 'text-area__element'"
		@input="textChangeHandler"
	/>
	<div class="error-container">
		<ErrorMessage :name="name" class="error-message" />
	</div>
</template>

<script>
	import { useField, ErrorMessage } from "vee-validate";

	export default {
		components: {
			ErrorMessage,
		},
		props: {
			label: String,
			name: String,
			type: String,
			className: String,
		},
		setup: (props) => {
			const { setValue } = useField(props.name);

			const textChangeHandler = (event) => {
				setValue(event.target.value);
			};

			return {
				setValue,
				textChangeHandler,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./VeeTextArea";
</style>
