<template>
	<div>
		<div class="card-container">
			<h3>Log in</h3>
			<form class="form" @submit="(e) => e.preventDefault()">
				<div class="form-control">
					<label for="email">Email address</label>
					<input type="text" name="email" />
				</div>
				<div class="form-control">
					<label for="password">Password</label>
					<input type="password" name="password" />
				</div>
				<div class="form-control">
					<ButtonComponent :text="'Log in'" :size="'normal'" />
				</div>
			</form>
		</div>
		<div class="forgot-password subtle-label" @click="navigateResetPassword">
			Forgot your password?
		</div>
	</div>
	<div class="additional-controls subtle-label">
		Already regsitred?
		<span @click="navigateRegister">Sign in</span>
	</div>
</template>

<script>
	import ButtonComponent from "@/components/Ui/Buttons/ButtonComponent";
	export default {
		props: {
			setForm: Function,
		},
		components: {
			ButtonComponent,
		},
		setup: (props) => {
			const navigateResetPassword = () => {
				props.setForm("reset");
			};

			const navigateRegister = () => {
				props.setForm("register");
			};

			return {
				navigateResetPassword,
				navigateRegister,
			};
		},
	};
</script>

<style lang="scss" scoped>
	@import "./LoginForm";
</style>
