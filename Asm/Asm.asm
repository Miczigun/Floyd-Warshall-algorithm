.data
S_MAX equ 0x3E80
n word ?
it word ?

.code
asmFloydWarshall proc
	mov n, dx
	mov it, bx

	xor     rax, rax
    xor     rbx, rbx


inner_loop_start:
    ; Compute indices
    mov     r10, rax               ; r10 = i
    imul    r10, n                ; r10 = i * n
    add    r10, it               ; r10 = i * n + iteration

    mov     r11, it                ; r11 = iteration
    imul    r11, n                ; r11 = iteration * n
    add     r11, rbx               ; r11 = iteration * n + j

    mov     r12, rax               ; r12 = i
    imul    r12, n               ; r12 = i * n
    add     r12, rbx               ; r12 = i * n + j

    
    vmovdqu16  ymm0, [rdi + r10*2] ;Load resultGraph[i * rAndC + iteration]

    
    vmovdqu16  ymm1, [rdi + r11*2] ;Load resultGraph[iteration * rAndC + j]

    vmovdqu16  ymm2, [rdi + r12*2] ;Load resultGraph[i * rAndC + j]

    vpaddw ymm3, ymm1, ymm0
    vpminw ymm4, ymm3, ymm2


    ; Update resultGraph[i * rAndC + j]
    vmovdqu16 [rdi + r12*2], ymm4

inner_loop_increment:
    inc     rbx
    cmp     rbx, n
    je      inner_loop_end
    jl      inner_loop_start

inner_loop_end:
    inc     rax
    cmp     rax, n
    je      outer_loop_end
    jl      inner_loop_start

outer_loop_end:
	ret

asmFloydWarshall endp
end