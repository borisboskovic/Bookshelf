<template>
	<div class="navbar-item-wrapper">
		<router-link :to="to" :active-class="activeClass">
			<slot />
		</router-link>
	</div>
</template>

<script>
	export default {
		props: {
			to: {
				type: String,
				required: true,
			},
			activeClass: {
				type: String,
				required: false,
				default: "active-link",
			},
		},
	};
</script>

<style lang="scss" scoped>
	@import "./NavbarItem";
</style>
