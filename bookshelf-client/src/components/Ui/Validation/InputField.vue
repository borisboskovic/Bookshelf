<template>
	<label :for="name" class="input-label">{{ label }}</label>
	<Field :name="name" :type="type" class="input-component" />
	<div class="error-container">
		<ErrorMessage :name="name" class="error-message" />
	</div>
</template>

<script>
	import { Field, ErrorMessage } from "vee-validate";

	export default {
		components: {
			Field,
			ErrorMessage,
		},
		props: {
			label: String,
			name: String,
			type: String,
		},
	};
</script>

<style lang="scss" scoped>
	@import "./InputField";
</style>
