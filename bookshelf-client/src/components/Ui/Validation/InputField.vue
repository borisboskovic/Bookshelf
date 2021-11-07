<template>
	<label :for="name" class="input-label">{{ label }}</label>
	<div class="field-container">
		<Field :name="name" :type="fieldType" :class="fieldClasses" />
		<i
			v-if="type === 'password' && peekPassword"
			class="fa fa-eye"
			@click="togglePeekPassword"
		/>
		<i
			v-if="type === 'password' && !peekPassword"
			class="fa fa-eye-slash"
			@click="togglePeekPassword"
		/>
	</div>
	<div class="error-container">
		<ErrorMessage :name="name" class="error-message" />
	</div>
</template>

<script>
	import { ref, computed } from "vue";
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
		setup: (props) => {
			const peekPassword = ref(false);

			const togglePeekPassword = () => {
				peekPassword.value = !peekPassword.value;
			};

			const fieldType = computed(() => {
				if (props.type !== "password") {
					return props.type;
				} else if (!peekPassword.value) {
					return "password";
				} else {
					return "text";
				}
			});

			const fieldClasses = `input-component ${
				fieldType.value === "password" && "field-with-icon"
			}`;

			return {
				peekPassword,
				togglePeekPassword,
				fieldType,
				fieldClasses,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./InputField";
</style>
